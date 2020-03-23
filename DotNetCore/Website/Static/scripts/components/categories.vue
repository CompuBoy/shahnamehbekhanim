<template>
	<div id="categories">
		<ul>
			<li 
                v-for="(item, index) in items"
                v-bind:key="index"
                v-bind:class="{ current: item === value }"
            >
				<a v-on:click="$emit('input', item)">{{ item || 'خانه' }}</a>
			</li>
		</ul>
	</div>
</template> 

<script lang="ts">
    import Api from '../services';
    import Vue from 'vue';
    
    export default Vue.extend({
        data() {
            return {
                items: [],
            }
        },
        props: [
            'value',
        ],
        methods: {
            fetch() {
                Api.categories().then((items: any[]) => {
                    this.items = ['', ...items] as any;
                });
            }
        },
        created() {
            this.fetch();
        }
    });
</script>
