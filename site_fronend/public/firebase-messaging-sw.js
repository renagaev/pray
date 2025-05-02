
importScripts('https://www.gstatic.com/firebasejs/8.2.1/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/8.2.1/firebase-messaging.js');

// Initialize the Firebase app in the service worker by passing in
// your app's Firebase config object.
// https://firebase.google.com/docs/web/setup#config-object
const firebaseConfig = {
    apiKey: "AIzaSyDRQLpksW1vPe7J56Av4kUCJTn9Qvn2490",
    authDomain: "pray-79b26.firebaseapp.com",
    projectId: "pray-79b26",
    storageBucket: "pray-79b26.appspot.com",
    messagingSenderId: "709819499706",
    appId: "1:709819499706:web:81d27fe6021bfe199f2e83",
    measurementId: "G-M0C4Q970K5"
};
firebase.initializeApp(firebaseConfig)
// Retrieve an instance of Firebase Messaging so that it can handle background
// messages.
const messaging = firebase.messaging();