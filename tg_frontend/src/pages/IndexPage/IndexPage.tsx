import {List} from '@telegram-apps/telegram-ui';
import type {FC} from 'react';

import {Page} from '@/components/Page.tsx';

import RequestForm from "@/components/RequestForm/RequestForm.tsx";

export const IndexPage: FC = () => {
    return (
        <Page back={false}>
            <List>
                <RequestForm></RequestForm>
            </List>
        </Page>
    );
};
