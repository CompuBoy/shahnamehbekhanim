<template>
    <div id="news">
        <h3>تازه ها</h3>      
        <ul>  
            <li v-for="(item, index) in itemsView"
                v-bind:key="index">
                <router-link v-bind:to="'/news/' + item.id">
                    <i>{{ item.date }}</i>                    
                    <strong>{{ item.title }}</strong>
                    <i>{{ item.subtitle }}</i>
                    <small>{{ item.content }}</small>
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

        if (item.subtitle && item.subtitle.indexOf(search) >= 0) {
            return true;
        }

        if (item.content && item.content.indexOf(search) >= 0) {
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
            'id',
            'title',
            'subtitle',
            'content',
            'search'
        ],
        watch: {
            title(){ this.update() },
            subtitle(){ this.update() },
            content(){ this.update() },
            date(){ this.update() }  
        },
        methods: {
            update() {
                this.loading = true;
                Api.newscontent(this.id)
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