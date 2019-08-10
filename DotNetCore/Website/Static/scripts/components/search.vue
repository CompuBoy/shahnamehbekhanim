<template>
	<div id="search">
		<input 
			v-bind:value="value"
	   		v-on:input="$emit('input', $event.target.value)"
		/>
	</div>
</template>

<script lang="ts">
    import Vue from 'vue';
    
    export default Vue.extend({
        data() {
            return {
            }
        },
        props: [
            'value',
        ],
        methods: {
        },
        created() {
        }
    });
</script>
