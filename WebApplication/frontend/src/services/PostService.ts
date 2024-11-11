import {Post} from "@/services/Post";
import {VoteType} from "@/services/Post";
import {config} from "@/services/apiConfig"
import {RequestHelper} from "@/services/RequestHelper";
import {getMessaging, getToken} from "firebase/messaging"

export class PostService {
    private static _instance: PostService


    public static get Instance() {
        return this._instance || (this._instance = new this())
    }

    baseUrl = `${config.baseUrl}/post`

    public items: Post[] = []

    async load(): Promise<void> {
        this.items = await RequestHelper.get(`${this.baseUrl}`)
    }

    async save(id: number, post: Post) {
        await RequestHelper.put(`${this.baseUrl}/${id}`, JSON.stringify(post))
    }

    async loadAdmin(): Promise<Post[]> {
        return await RequestHelper.get(`${this.baseUrl}/forAdmin`)
    }

    async vote(item: Post, type: VoteType) {
        switch (type) {
            case VoteType.Standard:
                item.votes++
                break
            case VoteType.LargeGroup:
                item.largeGroupVotes++
                break
        }
        const params = new URLSearchParams({id: String(item.id), voteType: String(type)})
        const voted = this.loadVotes()
        voted.push({id: item.id, date: new Date(), type: type})
        this.saveVotes(voted)
        return RequestHelper.post(`${this.baseUrl}/vote?` + params)
    }

    saveVotes(votes) {
        window.localStorage.setItem('votes2', JSON.stringify(votes))
    }


    canVote(id: number) {
        const voted = this.loadVotes()
        return !voted.some(x => x.id == id && new Date(x.date) > new Date(new Date().valueOf() - 60 * 60000))
    }

    loadVotes(): { id: number; date: Date; type: VoteType }[] {
        let res = []
        try {
            const json = window.localStorage.getItem('votes2')
            if (json) {
                res = JSON.parse(json)
            }
        } catch (e) {
            res = []
        }
        return res
    }

    canSuggest() {
        const last = window.localStorage.getItem("lastSuggest")
        if (last == null) return true
        const lastDate = new Date(Number(last))
        return lastDate < new Date(Date.now().valueOf() - 10 * 60000)
    }

    suggest(text: string, author: string, token: string | null) {
        window.localStorage.setItem("lastSuggest", String(Date.now().valueOf()))
        const params = new URLSearchParams({text: text, author: author, token: token})
        return RequestHelper.post(`${this.baseUrl}/suggest?` + params)
    }

    isSubscribed() {
        return window.localStorage.getItem('subscribed') == 'true'
    }

    async subscribe() {
        try {
            const token = await getToken(getMessaging(), {vapidKey: "BJqp6O1Tj6zmiH726zHSrx9UClk-2Gm-LIjltiO6jifHVh-zoV--N90eHrCcEGxcQt_T1E-SXKojBOZaXLIuYZg"})
            const params = new URLSearchParams({token: token})
            await RequestHelper.post(`${this.baseUrl}/subscribe?` + params)
            window.localStorage.setItem('subscribed', 'true')
        } catch (e) {
            console.log(e)
        }
    }


}