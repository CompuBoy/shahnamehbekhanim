import Vue from 'vue';
import Categories from './components/categories.vue';
import Posts from './components/posts.vue';
import Post from './components/post.vue';
import PodcastPosts from './components/podcastposts.vue';
import PodcastPost from './components/podcastpost.vue';
import Search from './components/search.vue';
import Api from './services';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const router = new VueRouter({
   //mode: 'history',
   routes: [
       { name: 'index', path: '/', component: Posts, props: r => ({ 
               category: r.query.category,
               narrator: r.query.narrator,
               search: r.query.search 
       }) },
       { name: 'post', path: '/post/:id', component: Post, props: true },
       { name: 'podcastposts', path: '/podcastposts/', component: PodcastPosts, props: true },
       { name: 'podcastpost', path: '/podcastpost/:id', component: PodcastPost, props: true }
   ]
});

const app = new Vue({
    el: '#app',
    data: {
        category: '',
        search: '',
        showCategories: true,
    },
    methods: {
        updateCategories() {
            this.showCategories = this.$route.name != 'post';
        }
    },
    created: function(){
        this.updateCategories();
    },
    watch: {
        category: function(category) {
            router.push({ path: '/', query: { category }});
        },
        search: function (search) {
            router.replace({ path: '/', query: { category: this.category, search }});
        },
        '$route': function(to, from) {
            if (to.path === '/') {
                this.category = to.query.category;
            }

            this.updateCategories();
        }
    },
    components: {
        'categories': Categories,
        'search': Search,
        'posts': Posts,
        'podcastposts': PodcastPosts,
        'podcastpost': PodcastPost,
    },
    router,
});

console.log(app);
