<template>
  <div>
    <Footer/>
    <CardList/>
  </div>
</template>

<script>
import {Component, Vue} from "vue-property-decorator";
import CardList from "@/components/CardList.vue";
import Footer from "@/components/Footer.vue";

@Component({
  components: {
    Footer,
    CardList
  }
})
export default class UserScreen extends Vue {
}
</script>

<style scoped>

</style>