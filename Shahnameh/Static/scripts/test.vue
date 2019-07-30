<template>
	<div id="test">
		<h1>My Todo App!</h1>
        {{ test }}
		<ul>
			<li 
                v-for="(item, index) in items"
                v-on:click="$emit('input', item)"
                v-bind:key="index"
                v-bind:class="{ current: item === value }"
            >
				{{ item }}
			</li>
		</ul>
	</div>
</template>

<script lang="ts">
export default {
	data() {
		return {
            test: 'sosis'
        }
	},
	props: [
	    'value',
	    'items',
	]
}
</script>

<style lang="css">
    #test li.current {
        background: red;
    }
</style>