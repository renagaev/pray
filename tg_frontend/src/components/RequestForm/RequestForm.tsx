import {Button, Input, Textarea} from "@telegram-apps/telegram-ui";
import React from "react";

function RequestForm() {
    const [author, setAuthor] = React.useState("")
    const [text, setText] = React.useState("")

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
            <Button>Оправить</Button>
        </div>

    )
}

export default RequestForm