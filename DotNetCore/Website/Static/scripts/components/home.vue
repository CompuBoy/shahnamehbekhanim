<template>
    <div id="home">
        <router-link v-bind:to="'/posts'" class="postbox">                                 
            <img class="play">
        </router-link>
        <ul>
            <li class="box">
                <router-link v-bind:to="'/about'">                                        
                    <img class="thumbnail aboutthumb">
                    <strong>داستان ما</strong>
                    <i>روایت پا گرفتن کانال شاهنامه بخوانیم و سایت</i>
                </router-link>
            </li>
            <li class="box">
                <router-link v-bind:to="'/news'">
                    <img class="thumbnail newsthumb">                                        
                    <strong>انتشار پادکست</strong>
                    <i>پادکست شاهنامه بخوانیم منتشر شد</i>
                </router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    
    export default Vue.extend({
       created() {},
    });
</script>