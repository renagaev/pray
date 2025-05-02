<template>
  <div class="container">
    <footer class="footer has-text-centered" style="background-color: white">
      <p class="subtitle" style="max-width: 600px; white-space: pre-wrap; margin: auto">Огонь непрестанно пусть горит на жертвеннике и не угасает. Левит 6:13</p>
      <b-button v-if="needSubscribe"
                @click="subscribe"
                class="is-success is-centered" size="is-medium" style="margin: 1rem">Подписаться
      </b-button>
      <h1 class="title is-size-2-mobile is-size-1-desktop is-size-1-tablet" style="margin: 0.5rem; text-align: center">
        Жертвенник</h1>
      <div style="text-align: center">
        <b-button class="is-success" size="is-medium" :disabled="!canSuggest" @click="openForm"
                  style="margin: 1rem">
          Предложить
          нужду
        </b-button>
      </div>
    </footer>
    <b-modal v-model="formOpened">
      <SuggestForm @close="closeForm"/>
    </b-modal>
  </div>

</template>

<script lang="ts">
import {Component, Prop, Vue} from "vue-property-decorator";
import {PostService} from "@/services/PostService";
import SuggestForm from "@/components/SuggestForm.vue";
import {BButton} from 'buefy/dist/esm/button'
import {BModal} from 'buefy/dist/esm/modal'
import {isSupported} from "firebase/messaging"

@Component({
  components: {SuggestForm, 'b-button': BButton, 'b-modal': BModal}
})
export default class HelloWorld extends Vue {
  formOpened = false
  canSuggest = PostService.Instance.canSuggest()
  needSubscribe = false
  
  async mounted(){
    this.needSubscribe = !PostService.Instance.isSubscribed() && await isSupported()
  }

  async subscribe() {
    await PostService.Instance.subscribe()
  }

  openForm() {
    this.formOpened = true
  }

  closeForm() {
    this.canSuggest = false
    this.formOpened = false
  }
}
</script>

<style>
footer {
  padding: 1rem 1rem 1rem !important;
  background-color: white;
}
</style>