<template>
	<div id="posts">
        <h3>{{ category }}</h3>
        <div v-if="narrator" class="narrator">
            نمایش پست های با گویندگی 
            {{ this.narrator }}.
            برای نمایش همه پستها
            <router-link to="?">اینجا را کلیک نمایید</router-link>.
        </div>
        <div v-if="loading">Loading...</div>
		<ul v-if="!loading">
			<li 
                v-for="(item, index) in itemsView"
                v-bind:key="index"
            >
                <router-link v-bind:to="'/post/' + item.id">
                    <i>{{ item.id }}</i>
                    <strong>{{ item.title }}</strong>
                    <small>{{ item.category }} / گوینده: {{ item.narrator }}</small>
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
                    .filter((i: any) => search(i, this.search))
                    .filter((i: any) => !this.narrator || i.narrator === this.narrator);
            }
        },
        props: [
            'category',
            'narrator',
            'search'
        ],
        watch: {
            category(){ this.update() },
            narrator(){ this.update() },
        },
        methods: {
            update() {
                this.loading = true;
                Api.posts(this.category == '-' || this.category == '*' ? '' : this.category, this.narrator)
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