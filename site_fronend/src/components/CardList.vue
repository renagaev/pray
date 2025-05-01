<template>
  <div ref="container" class="container">

    <div class="columns is-centered is-multiline" style="margin-left: 0; margin-right: 0">
      <div
          v-for="(item, i) in items"
          :key="i"
          class="column is-two-thirds-desktop is-two-thirds-tablet"
          style="margin-bottom: 25px">
        <Req :item="item" ></Req>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from "vue-property-decorator";
import {Post} from "@/services/Post";
import {PostService} from "@/services/PostService";
import Req from "@/components/Req.vue";

@Component({
  components: {Req}
})
export default class HelloWorld extends Vue {
  items: Post[] = []

  async created() {
    await PostService.Instance.load()
    this.items = PostService.Instance.items
  }
}
</script>

<style>
span.icon.has-icons-right.is-large {
  margin-left: 40px;
}

span.icon.has-icons-left.is-large {
  margin-right: 40px;
}
</style>