<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
import CardList from "@/components/CardList.vue";
import Footer from "@/components/Footer.vue";
@Component({
  components: {
    Footer,
    CardList
  }
})
export default class App extends Vue {
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
