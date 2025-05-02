import Vue from 'vue'
import Router, {Route} from 'vue-router'
import UserScreen from "@/components/UserScreen.vue"
Vue.use(Router);

const admin =  () => import("@/components/admin/AdminContainer.vue")
export default new Router({
    routes: [
        {
            path: "/",
            component: UserScreen
        },
        {
            path: "/admin",
            component: admin
        },
        {
            path: "/:id",
            component: UserScreen
        },

    ]
})