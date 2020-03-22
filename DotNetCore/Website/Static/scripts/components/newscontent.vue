<template>
    <div id="newscontent">
        <div>
            <div class="header">
                <h4>{{ info.title }}</h4>
                <h5>{{ info.subtitle }}</h5>
                <small>{{ info.date }}</small>                
            </div>
            <div class="content">
                <div class="content" v-if="info.content">
                    {{ info.content }}
                </div>                
            </div>
        </div>
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
            'content',
            'date',
            'references',
            'search',
        ],
        watch: {
            content(){ this.update() },
            title(){ this.update() },
            references() { this.update() }
        },               
    });
</script>