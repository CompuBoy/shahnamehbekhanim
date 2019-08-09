//const apiBase = 'http://www.shahnamehbekhanim.com/api/';
const apiBase = '/api/';

function serializeQuery(obj: any, prefix?: string) : string {
    let str: string[] = [],
        p;

    for (p in obj) {
        if (obj.hasOwnProperty(p) && typeof(obj[p]) !== 'undefined') {
            const k = prefix ? prefix + "[" + p + "]" : p,
                v = obj[p];
            str.push((v !== null && typeof v === "object") ?
                serializeQuery(v, k) :
                encodeURIComponent(k) + "=" + encodeURIComponent(v));
        }
    }

    return str.join("&");
}

function request<T>(url: string, data:any = null): Promise<T> {
    return new Promise<T>((resolve, reject) => {
        fetch(`${apiBase}${url}` + (data ? `?${serializeQuery(data)}` : ''), { 
                //mode: 'no-cors',
                method: 'GET',
            })
            .then(r => { return r.text(); })
            .then(result => resolve(JSON.parse(result)));
    });
}

export default {
    categories: () => request<TCategories>('categories'),
    posts: (category: string = '', narrator?: string, from?: number,  count?: number) =>
        request<TPosts>('posts', { category, narrator, from, count }),
    podcastposts: (category: string = '', from?: number, count?: number) => request<TPodcastPosts>('podcastposts', { category, from, count }),
    post: (id: number) => request<TPost>('post/' + id),
    podcastpost: (id: string) => request<TPodcastPost>('podcastpost/' + id),
    verses: (id: number) => request<TVerses>('verses/', { postId: id }),
    phrases: (id: number) => request<TPhrases>('phrases/', { postId: id }),
    persons: (id: number) => request<TPersons>('persons/', { postId: id }),
}

export type TCategories = string[];

export type TPosts = {
    id: number,
    category: string,
    title: string,
    narrator: string,
    story: string,
}[];

export type TPost = {
    ID: number;
    Category: string;
    Title: string;
    StartingVerse: number;
    EndingVerse: number;
    Narrator: string;
    Story: string;
};

export type TPodcastPosts = {
    id: string;
    category: string;
    title: string;
    story: string;
    date: string;
    url: string;
}[];

export type TPodcastPost = {
    id: string;
    category: string;
    title: string;
    story: string;
    date: string;
    description: string;
    url: string;
};

export type TVerses = {
    index: number,
    part1: {
        start: number,
        end: number, 
    },
    part2 : {
        start: number,
        end: number, 
    },
}[];

export type TPhrases = {
    value: string, 
    description: string, 
    postId: number, 
    verse: number, 
    inPart1: boolean,
    inPart2: boolean,
}[];

export type TPersons = string[];
