<template>
  <div>
    <b-field label="Текст">
      <b-input v-model="text" type="textarea"></b-input>
    </b-field>
    <b-field label="Автор">
      <b-input v-model="author"></b-input>
    </b-field>
    <div class="columns is-multiline" style="align-items: center; display: flex">
      <div class="column p">
        <b-switch v-model="published" :disabled="post.published">Опубликовано</b-switch>
      </div>
      <div class="column p">
        <b-switch v-model="hidden">Скрыто</b-switch>
      </div>
      <div class="column p">
        <b-button class="is-success" @click="save">Сохранить</b-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue} from "vue-property-decorator";
import {Post} from "@/services/Post";
import {BField} from 'buefy/dist/esm/field'
import {BInput} from 'buefy/dist/esm/input'
import {BSwitch} from "buefy/src/components/switch"
import {BButton} from "buefy/dist/cjs/button";
import {PostService} from "@/services/PostService";

@Component({
  components: {
    'b-field': BField,
    'b-input': BInput,
    'b-button': BButton,
    "b-switch": BSwitch,
  }
})
export default class PostEdit extends Vue {
  @Prop() private post: Post
  text = ""
  author = ""
  hidden = false
  published = false

  mounted(){
    this.text = this.post.text
    this.author = this.post.author
    this.hidden = this.post.hidden
    this.published = this.post.published
  }
  async save() {
    const post = this.post
    post.published = this.published
    post.hidden = this.hidden
    post.text = this.text
    post.author = this.author
    await PostService.Instance.save(this.post.id, this.post)
  }
}
</script>

<style scoped>
.p {
  padding: 10px;
}
</style>