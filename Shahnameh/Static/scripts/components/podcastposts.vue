<template>
    <div id="podcastposts">
        <h3>{{ category }}</h3>
        <div v-if="loading">در حال بارگذاری...</div>
        <ul v-if="!loading">
            <li v-for="(item, index) in itemsView"
                v-bind:key="index">
                <router-link v-bind:to="'/podcastpost/' + item.id">
                    <i>{{ item.date }}</i>
                    <strong>{{ item.title }}</strong>
                    <small>{{ item.description }}</small>
                </router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import Api from '../services';

    const search = (item, search) => {
        if (!search) {
            return true;
        }

        if (item.title && item.title.indexOf(search) >= 0) {
            return true;
        }

        if (item.story && item.story.indexOf(search) >= 0) {
            return true;
        }

        return  false;
    };

    export default Vue.extend({
        data() {
            return {
                loading: true,
                items: [],
            }
        },
        computed: {
            itemsView: function () {
                return this.items
                    .filter((i: any) => search(i, this.search));
            }
        },
        props: [
            'category',
            'title',
            'description',
            'search'
        ],
        watch: {
            category(){ this.update() },
            title(){ this.update() },
            description() { this.update() },
        },
        methods: {
            update() {
                this.loading = true;
                Api.podcastposts(this.category)
                    .then((r: any) =>  {
                        this.items = r;
                        this.loading = false;
                        console.log(r);
                    });
            }
        },
        created() {
            this.update();
        }
    });
</script>