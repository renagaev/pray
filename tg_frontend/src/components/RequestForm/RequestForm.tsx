import {Input, Textarea, Snackbar} from "@telegram-apps/telegram-ui";
import {mainButton} from "@telegram-apps/sdk-react";
import React from "react";
import RequestsService from "@/services/RequestsService";


function RequestForm() {
    const [author, setAuthor] = React.useState("");
    const [text, setText] = React.useState("");
    const [snackbarOpen, setSnackbarOpen] = React.useState(false);

    // refs для свежих значений
    const textRef = React.useRef(text);
    const authorRef = React.useRef(author);
    React.useEffect(() => {
        textRef.current = text;
        authorRef.current = author;
    }, [text, author]);

    // монтируем mainButton единожды
    React.useEffect(() => {
        if (!mainButton.isMounted()) {
            mainButton.mount();
            mainButton.setParams({ text: "Отправить", isEnabled: true, isVisible: true });
            mainButton.onClick(async () => {
                await RequestsService.submitRequest(textRef.current, authorRef.current);
                setText("");
                setAuthor("");
                setSnackbarOpen(prev => !prev);
            });
        }
    }, []);

    return (
        <div>
            <Textarea
                header="Нужда"
                value={text}
                onChange={e => setText(e.target.value)}
                placeholder="Напишите здесь вашу нужду"
            />
            <Input
                header="Ваше имя"
                value={author}
                onChange={e => setAuthor(e.target.value)}
            />
            {snackbarOpen && (
                <Snackbar
                    duration={4000}
                    description="Скоро она будет опубликована"
                    onClose={() => setSnackbarOpen(false)}
                >
                    Ваша нужда отправлена
                </Snackbar>
            )}
        </div>
    );
}

export default RequestForm