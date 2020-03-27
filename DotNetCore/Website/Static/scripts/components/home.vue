<template>
    <div id="home">       
        <div class="row">
            <div class="col-12 col-md-6 box">
                <router-link v-bind:to="'/about'">                                        
                    <img class="thumbnail aboutthumb">
                    <div class="text-padding">
                        <strong>داستان ما</strong>
                        <i>روایت پا گرفتن کانال شاهنامه بخوانیم و سایت</i>
                    </div>
                </router-link>
            </div>
            <div class="col-12 col-md-6 box">
                <router-link v-bind:to="'/news'">
                    <img class="thumbnail newsthumb">
                    <div class="text-padding">                                       
                        <strong>انتشار پادکست</strong>
                        <i>پادکست شاهنامه بخوانیم منتشر شد</i>
                    </div>
                </router-link>
            </div>
        </div>
        <div class="row">
            <posts v-model="posts"></posts>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import Posts from './posts.vue'
    
    export default Vue.extend({
       created() {},
       components: {
           'posts': Posts
       }
    });
</script>