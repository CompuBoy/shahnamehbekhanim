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
    import Api, { TNews, TNewsContent } from '../services';

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
                info: {} as TNewsContent,
            }
        },
        props: [
            'id'
        ],
        watch: {
            id: function() { this.update() },
        },
        methods: {
            back() {
                history.back();
            },
            update() {
                Promise.all([
                    Api.newscontent(this.id),
                ]).then(([info]) => {
                    console.log(info);

                    this.loading = false;
                    this.info = info;
                });
            },
        },
        created() {
            this.update();
        },
        destroyed() {
        }
    });
</script>