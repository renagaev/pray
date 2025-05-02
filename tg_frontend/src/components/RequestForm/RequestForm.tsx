import {Input, Textarea} from "@telegram-apps/telegram-ui";
import {mainButton} from "@telegram-apps/sdk-react";
import React from "react";
import RequestsService from "@/services/RequestsService";

function RequestForm() {
    const [author, setAuthor] = React.useState("")
    const [text, setText] = React.useState("")

    if (!mainButton.isMounted()) {
        mainButton.mount()
        mainButton.setParams({
            text: "Отправить"
        })
        mainButton.onClick(async () => {
            await RequestsService.submitRequest(text, author)
            mainButton.setParams({
                text: "Отправлено"
            })
            setText("")
            setAuthor("")
        })
    }


    return (
        <div>
            <Textarea
                header="Нужда"
                value={text}
                onChange={event => setText(event.target.value)}
                placeholder="Напишите здесь вашу нужду"
            ></Textarea>
            <Input
                header="Ваше имя"
                value={author}
                onChange={event => setAuthor(event.target.value)}
            ></Input>
        </div>
    )
}

export default RequestForm