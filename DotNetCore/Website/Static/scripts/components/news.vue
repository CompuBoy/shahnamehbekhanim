<template>
    <div id="news">
        <h3>انتشار پادکست شاهنامه بخوانیم</h3> 
        <p>
        پادکست «شاهنامه بخوانیم» در واقع قالب جدیدی‌ست برای انتشار فایل‌های کانال تلگرامی <a href="
https://t.me/ShahnamehBekhanim" target="_blank"> «شاهنامه بخوانیم» </a> و همین وب‌سایت ما.
        </p>
        <p>
       برای خواندن شاهنامه باید انتخاب کنید که کدام پیرایش باید خوانده شود. پیرایش‌های شاهنامه با هم تفاوت دارند.<a href="https://sokhanpub.net/product-category/%d9%85%d8%ac%d9%85%d9%88%d8%b9%d9%87-%d8%a2%d8%ab%d8%a7%d8%b1-%d8%ac%d9%84%d8%a7%d9%84-%d8%ae%d8%a7%d9%84%d9%82%db%8c-%d9%85%d8%b7%d9%84%d9%82/" target="_blank"> پیرایش دوم دکتر خالقی مطلق</a>، منتشر شده توسط انتشارات سخن، متنی‌ست که ما آن را انتخاب کرده و خوانده‌ایم. نکته دیگر اینکه، از آنجاییکه این متن همراه با علامات سجاوندی پیرایش شده بود ما نیز سعی کرده‌ایم در خوانش به این علامات وفادار بمانیم. این بدان معناست که خوانش برخی واژه‌ها با چیزی که امروزه گفته می‌شود و به گوش آشناست، متفاوت است. اینکه ما که هستیم و قصه‌ی این شاهنامه خواندن از کی  و چگونه آغاز شد،  چند خط پایین‌تر نوشته شده است. اما درباره پادکست؛ باید بدانید که بهترین راه شنیدن‌اش با برنامه‌های پادکست‌خوان یا پادگیرهاست. آدرس پادکست‌ «شاهنامه بخوانیم» در چند برنامه‌ ثبت شده است که می‌توانید آنها را در گوشه همبن صفحه بینید. با امکانات این برنامه‌ها می‌توانید سرعت خوانش را به دلخواه کم و زیاد کنید، از انتشار قسمت جدید خبردار شوید و خیلی موارد بیشتر که به کارتان خواهد آمد. استفاده از این برنامه‌ها به ما نیز در برنا‌مه‌ریزی برای آینده پادکست کمک فراوانی می‌کند.
        </p>     
        <a class="castbox" href="https://castbox.fm/va/1659597"></a>
        <a class="podcastplayer" href="https://castbox.fm/x/1yVpT"></a>
        <a class="applepodcast" href="https://podcasts.apple.com/us/podcast/shahnameh/id1446178172"></a>
        <a class="overcast" href="https://overcast.fm/+Px-7lLgz0"></a>
        <a class="podcastaddict" href="https://shahnameh.podbean.com/feed.xml via @PodcastAddict"></a>
        
        <ul v-if="showNews" >  
            <li v-for="(item, index) in itemsView"
                v-bind:key="index">
                <router-link v-bind:to="'/news/' + item.id">                                        
                    <strong>{{ item.title }}</strong>
                    <i>{{ item.subtitle }}</i>
                    <i>{{ item.date }}</i>
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
            'date',
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