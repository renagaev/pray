import Vue from "vue";
import App from "./App.vue";
import './main.scss'
import {initializeApp} from "firebase/app"

Vue.config.productionTip = false;
import router from "@/routes/router"

const firebaseConfig = {
    apiKey: "AIzaSyDRQLpksW1vPe7J56Av4kUCJTn9Qvn2490",
    authDomain: "pray-79b26.firebaseapp.com",
    projectId: "pray-79b26",
    storageBucket: "pray-79b26.appspot.com",
    messagingSenderId: "709819499706",
    appId: "1:709819499706:web:81d27fe6021bfe199f2e83",
    measurementId: "G-M0C4Q970K5"
};
initializeApp(firebaseConfig)
const v = new Vue({
    render: h => h(App),
    router: router
})


v.$mount("#app");
