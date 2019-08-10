<template>
	<div id="post">
        <div v-if="loading">Loading...</div>
        <div v-if="!loading">
            <div class="header">
                <h4>{{ info.title }}</h4>
                <h5>گوینده: <router-link v-bind:to="'/?narrator=' + info.narrator">{{ info.narrator }}</router-link></h5>
                <div v-if="soundLoading" class="loading">
                    <span></span>
                </div>
                <div 
                    v-bind:class="{ controls: true, playing: soundPlaying }"
                    v-if="!soundLoading"
                >
                    <a class="play" v-on:click="play">Play</a>
                    <a class="stop" v-on:click="stop">Stop</a>
                    <div class="progress" v-on:click="seek">
                        <div v-bind:style="{ width: (soundPosition / soundDuration * 100) + '%' }"></div>
                    </div>
                    <strong v-html="formatedPosition"></strong>
                </div>
                <a class="back-btn" v-on:click="back"></a>
            </div>
            <div class="content">
                <div class="phrases" v-if="phrases.length > 0">
                    <div 
                        v-for="(phrase, i) in phrases" 
                        v-bind:key="i"
                        v-bind:class="{ current: isInCurrentVerse(phrase) }"
                        v-on:click="gotoPhrase(phrase)"
                    >
                        <strong>{{ phrase.value }}</strong>
                        <i>{{ phrase.description }}</i>
                    </div>
                </div>
                <div class="story" v-if="info.Story">
                    {{ info.Story }}
                </div>
            </div>
        </div>
	</div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import Api from '../services';
    import { TPost, TVerses, TPhrases, TPersons } from '../services';
    import { Howl } from 'howler';
    
    const soundsBase = '/api/voice?postId=';
    let sound: Howl;
    
    export default Vue.extend({
        data() {
            return {
                loading: true,
                soundLoading: true,
                soundPlaying: false,
                soundPosition: 0,
                soundDuration: 0,
                currentVerse: 0,
                currentPart: 0,
                info: {} as TPost,
                verses: [] as TVerses,
                phrases: [] as TPhrases,
                persons: [] as TPersons,
            }
        },
        computed: {
            formatedPosition: function() {
                function formatSeconds(sec) {
                    return [(sec / 3600), ((sec % 3600) / 60), ((sec % 3600) % 60)]
                        .map(v => Math.floor(v))
                        .map(v => v < 10 ? "0" + v : v)
                        .filter((i, j) => i !== "00" || j > 0)
                        .join(":");
                }

                return `${formatSeconds(this.soundPosition)} <span>/ ${formatSeconds(this.soundDuration)}</span>`;
            }
        },
        props: [
            'id'
        ],
        watch: {
            id: function() { this.update() },
            currentVerse: function () { setTimeout(() => this.scrollToCurrent(), 1); }
        },
        methods: {
            back() {
                history.back();
            },
            update() {
                Promise.all([
                    Api.post(this.id), 
                    Api.verses(this.id),
                    Api.phrases(this.id),
                    Api.persons(this.id),
                ]).then(([info, verses, phrases, persons]) => {
                    console.log(info, verses, phrases, persons);
                    
                    this.loading = false;
                    this.info = info;
                    this.verses = verses;
                    this.phrases = phrases;
                    this.persons = persons;
                    
                    sound = new Howl({
                        src: [`${soundsBase}${this.id}`],
                        format: ['mp3'],
                        onload: () => this.soundLoading = false,
                        onplay: () => requestAnimationFrame(this.updatePos),
                    });

                    this.play();
                });
            },
            updatePos() {
                this.soundPlaying = sound.playing();

                if (sound.state() != 'loaded') { return; }
                
                const pos = sound.seek() as number || 0;
                const verse = this.verses.find(v => pos >= v.part1.start && pos < v.part2.end);

                this.soundPosition = pos;
                this.soundDuration = sound.duration();

                if (verse) {
                    this.currentVerse = verse.index;
                    this.currentPart = pos < verse.part2.start ? 1 : 2;
                }
                
                requestAnimationFrame(this.updatePos);
            },
            play() {
                if (!sound.playing()) {
                    sound.play();
                } 
            },
            stop() {
                sound.pause();
            },
            seek(ev: MouseEvent) {
                const pos = ev.offsetX / (ev.currentTarget as HTMLDivElement).clientWidth * sound.duration();
                sound.seek(pos);
                this.updatePos();
            },
            isInCurrentVerse(phrase: any) {
                return this.currentVerse === phrase.verse && (
                    this.currentPart === 1 && phrase.inPart1 ||
                    this.currentPart === 2 && phrase.inPart2
                );
            },
            gotoPhrase(phrase: any) {
                let verse = this.verses.find(v => v.index === phrase.verse);
                if (verse) {
                    let pos = phrase.inPart1 ? verse.part1.start : verse.part2.start;
                    sound.seek(pos);
                    this.play();
                }
            },
            scrollToCurrent() {
                const current = this.$el.querySelector('.phrases .current') as any;
                if (!current) return; 
                if (current.scrollIntoViewIfNeeded) {
                    current.scrollIntoViewIfNeeded();
                } else {
                    current.scrollIntoView();
                }
            }
        },
        created() {
            this.update();
        },
        destroyed() {
            sound.unload();
            this.stop();
        }
    });
</script>
