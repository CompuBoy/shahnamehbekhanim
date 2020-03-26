<template>
    <div id="home">
        <router-link v-bind:to="'/posts'" class="row img-responsive postbox">                                 
            <img class="play">
        </router-link>
        <div class="row">
            <div class="col-md-6 box">
                <router-link v-bind:to="'/about'">                                        
                    <img class="thumbnail aboutthumb">
                    <div class="text-padding">
                        <strong>داستان ما</strong>
                        <i>روایت پا گرفتن کانال شاهنامه بخوانیم و سایت</i>
                    </div>
                </router-link>
            </div>
            <div class="col-md-6 box">
                <router-link v-bind:to="'/news'">
                    <img class="thumbnail newsthumb">
                    <div class="text-padding">                                       
                        <strong>انتشار پادکست</strong>
                        <i>پادکست شاهنامه بخوانیم منتشر شد</i>
                    </div>
                </router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    
    export default Vue.extend({
       created() {},
    });
</script>