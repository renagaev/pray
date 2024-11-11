<template>
  <div class="container">
    <div class="card is-left">
      <div class="card-content">
        <b-field label="Ваша нужда">
          <b-input maxlength="200" type="textarea" v-model="text"></b-input>
        </b-field>
        <b-field label="Ваше имя" label-position="left">
          <b-input maxlength="50" v-model="author"></b-input>
        </b-field>
        <b-field v-if="showNotifSwitch" label="Вы можете получать уведомления, когда кто-то помолится за вашу нужду">
          <b-switch v-model="notifications">Уведомления</b-switch>
        </b-field>
        <b-button @click="suggest" type="is-success" class="is-pulled-right" style="margin:   0 0  1.5rem 0">Отправить
        </b-button>
      </div>

    </div>
  </div>
</template>

<script>
import {Component, Prop, Vue} from "vue-property-decorator";
import {PostService} from "@/services/PostService";
import {BField} from 'buefy/dist/esm/field'
import {BInput} from 'buefy/dist/esm/input'
import {BButton} from 'buefy/dist/esm/button'
import {BSwitch} from "buefy/dist/components/switch";
import firebase from "firebase/app";

@Component({
  components:
      {
        'b-field': BField,
        'b-input': BInput,
        'b-button': BButton,
        "b-switch": BSwitch,
      }
})
export default class HelloWorld extends Vue {
  text = ""
  author = ""
  notifications = true

  showNotifSwitch = firebase.messaging.isSupported()

  async suggest() {

    let token = null
    if(this.notifications){
      try {
        token = await firebase.messaging().getToken({vapidKey: "BJqp6O1Tj6zmiH726zHSrx9UClk-2Gm-LIjltiO6jifHVh-zoV--N90eHrCcEGxcQt_T1E-SXKojBOZaXLIuYZg"})
      } catch (e) {
        console.log(e)
      }
    }
    await PostService.Instance.suggest(this.text, this.author, token)
    this.$emit("close")
  }

}
</script>

<style scoped>

</style>