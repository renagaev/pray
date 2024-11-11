import {config} from "@/services/apiConfig";

export class AuthService {

    private static baseUrl = `${config.baseUrl}/auth`

    static getSavedCredentials() {
        const login = window.localStorage.getItem("login")
        const password = window.localStorage.getItem("password")
        if (!login)
            return null
        return {login, password}
    }

    static saveCredentials(login, password) {
        window.localStorage.setItem("login", login)
        window.localStorage.setItem("password", password)
    }

    static async isTokenValid(): Promise<boolean> {
        const credentials = this.getSavedCredentials()
        if (!credentials) {
            return false
        }
        const resp = await fetch(this.baseUrl, {headers: {"Authorization": `${credentials.login}:${credentials.password}`}})
        return resp.status == 200
    }
}