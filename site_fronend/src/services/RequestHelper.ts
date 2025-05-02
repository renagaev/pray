import {AuthService} from "@/services/AuthService";

export class RequestHelper {

    static get(url: string) {
        return this.request(url, "GET", null).then(x => x.json())
    }

    static post(url, json?) {
        return this.request(url, "POST", json)
    }

    static put(url, json) {
        return this.request(url, "PUT", json)
    }

    private static request(url: string, method: string, content: string) {
        const token = AuthService.getSavedCredentials()
        const params: RequestInit = {method: method, headers: {}}
        if (content) {
            params.body = content
            params.headers['content-type'] = 'application/json'
        }
        if (token) {
            params.headers['Authorization'] = `${token.login}:${token.password}`
        }
        return fetch(url, params)
    }

}