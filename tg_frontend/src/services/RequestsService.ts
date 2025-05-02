import {retrieveRawInitData} from '@telegram-apps/bridge';

class RequestsService {
    static async submitRequest(text: string, author: string): Promise<void> {
        const apiBase = import.meta.env.VITE_API_BASE_URL
        const initDataRaw = retrieveRawInitData();
        console.log(initDataRaw)
        const request = {
            text: text,
            author: author
        }

        await fetch(`${apiBase}/tg/posts/submit`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                "Authorization": `tma ${initDataRaw}`
            }
        })
    }
}

export default RequestsService