<template>
    <div id="post">
        <div>
            <div class="header">
                <h4>{{ info.title }}</h4>
                <h5>{{ info.category }}</h5>
                <div>
                    <iframe src="https://www.podbean.com/media/player/kff7q-a12a4b?from=yiiadmin&download=1&version=1&skin=10&btn-skin=102&auto=0&download=1&pbad=1" height="122" width="100%" frameborder="0" scrolling="no" data-name="pb-iframe-player"></iframe>
                </div>
            </div>
            <div class="content">
                <div class="story" v-if="info.story">
                    {{ info.story }}
                </div>
                <div class="phrases" v-if="info.description">
                    {{ info.description }}
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import Api from '../services';
    import { TPodcastPost, } from '../services';

    export default Vue.extend({
        data() {
            return {
                loading: true,
                info: {} as TPodcastPost,
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
                    Api.podcastpost(this.id),
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
