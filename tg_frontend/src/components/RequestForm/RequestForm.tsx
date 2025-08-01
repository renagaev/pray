import React, {useState, useEffect, useRef, ChangeEvent } from "react";
import { Input, Textarea, Snackbar } from "@telegram-apps/telegram-ui";
import { mainButton } from "@telegram-apps/sdk-react";
import RequestsService from "@/services/RequestsService";

type Status = "default" | "error" | "focused" | undefined;

const RequestForm: React.FC = () => {
    const [author, setAuthor] = useState<string>("");
    const [text, setText] = useState<string>("");
    const [snackbarOpen, setSnackbarOpen] = useState<boolean>(false);
    const [textStatus, setTextStatus] = useState<Status>("default");
    const [authorStatus, setAuthorStatus] = useState<Status>("default");

    const textRef = useRef<string>(text);
    const authorRef = useRef<string>(author);

    useEffect(() => {
        textRef.current = text;
    }, [text]);

    useEffect(() => {
        authorRef.current = author;
    }, [author]);

    useEffect(() => {
        if (!mainButton.isMounted()) {
            mainButton.mount();
            mainButton.setParams({
                text: "Отправить",
                isEnabled: true,
                isVisible: true,
            });
            mainButton.onClick(async () => {
                const isTextValid = textRef.current.trim() !== "";
                const isAuthorValid = authorRef.current.trim() !== "";

                setTextStatus(isTextValid ? undefined : "error");
                setAuthorStatus(isAuthorValid ? undefined : "error");

                if (!isTextValid || !isAuthorValid) {
                    return;
                }

                try {
                    await RequestsService.submitRequest(textRef.current, authorRef.current);
                    setText("");
                    setAuthor("");
                    setSnackbarOpen(true);
                } catch (error) {
                    console.error("Ошибка при отправке:", error);
                }
            });
        }
    }, []);

    const handleTextChange = (e: ChangeEvent<HTMLTextAreaElement>) => {
        const value = e.target.value;
        setText(value);
        setTextStatus(value.trim() !== "" ? undefined : "error");
    };

    const handleAuthorChange = (e: ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        setAuthor(value);
        setAuthorStatus(value.trim() !== "" ? undefined : "error");
    };

    return (
        <div>
            <Textarea
                header="Нужда"
                value={text}
                status={textStatus}
                onChange={handleTextChange}
                placeholder="Напишите здесь вашу нужду"
            />
            <Input
                header="Ваше имя"
                value={author}
                status={authorStatus}
                onChange={handleAuthorChange}
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
};

export default RequestForm;
