import {retrieveLaunchParams} from '@telegram-apps/sdk';

class RequestsService {
    static async submitRequest(text: string, author: string): Promise<void> {
        const apiBase = import.meta.env.VITE_API_BASE_URL
        const {initDataRaw} = retrieveLaunchParams();
        const request = {
            text: text,
            author: author
        }

        await fetch(`${apiBase}/tg/requests/submit`, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                "Authorization": `tma ${initDataRaw}`
            }
        })
    }
}

export default RequestsService