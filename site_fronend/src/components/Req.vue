<template>
  <div class="card bm--card-equal-height has-text-centered" style="padding: 0.5rem; min-height: 30vh" :class="{highlighted: highlighted}">
    <div class="card-header" style="box-shadow: none">

      <div class="is-flex is-justify-content-flex-end is-align-items-center"
           style="width: 100%; margin: 5px; gap: 1rem">
        <p style="opacity: 0.7">{{ formattedDate }}</p>
        <b-button
            @click="share">
          <i class="fas fa-share-alt"></i>
        </b-button>
      </div>

    </div>
    <div class="card-content is-align-items-center is-flex has-text-centered">
      <div class="has-text-centered" style="width: 100%;">
        <p class="is-size-4-mobile title">{{ item.text }}</p>
        <p class="subtitle is-size-5-mobile is-italic">— {{ item.author }}</p>
      </div>


    </div>
    <footer class="card-footer columns is-multiline">
      <div style="width: 100%; margin-bottom: 1rem;">
        <b-button icon-left="praying-hand"
                  @click="voteStandard"
                  size="is-medium"
                  class="is-success"
                  :disabled="!canVote"
                  style="width: 100%; white-space: pre-wrap">{{ "Молитва совершена " + item.votes }}<span
            v-if="!canVote">{{ "  " }}<i
            class="fas fa-praying-hands"/></span>
        </b-button>
      </div>
      <div style="width: 100%">
        <b-button icon-left="praying-hand"
                  @click="voteChurch"
                  size="is-medium"
                  class="is-success"
                  :disabled="!canVote"
                  style="width: 100%">Церковь молилась {{ item.largeGroupVotes }}<span
            v-if="!canVote">{{ "  " }}<i
            class="fas fa-praying-hands"/></span>
        </b-button>
      </div>

    </footer>
  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue} from "vue-property-decorator";
import {Post} from "@/services/Post";
import {PostService} from "@/services/PostService";
import {BButton} from 'buefy/dist/esm/button'
import {VoteType} from "@/services/Post";

@Component({components: {'b-button': BButton}})
export default class Req extends Vue {
  @Prop() private item!: Post;
 
  canVote = false;
  highlighted = false

  created(){
    this.canVote = PostService.Instance.canVote(this.item.id)
  }
  

  get formattedDate() {
    return new Date(this.item.publishDate).toLocaleDateString("ru-Ru")
  }

  async voteStandard() {
    await this.vote(VoteType.Standard)
  }

  async voteChurch() {
    await this.vote(VoteType.LargeGroup)
  }

  async share() {
    await navigator.share({
      text: this.item.text,
      title: "Нужда на жертвеннике",
      url: window.location.origin + "/#" + this.item.id
    })
  }
  
  mounted(){
    const id = Number(this.$route.params.id)
    if (id == this.item.id) {
      this.highlighted = true
      this.$el.scrollIntoView({behavior: "smooth"})
    }
  }

  async vote(type) {
    this.canVote = false
    await PostService.Instance.vote(this.item, type)
  }
}
</script>

<style lang="scss">
.bm--card-equal-height {
  display: flex;
  flex-direction: column;
  height: 100%;
}

.bm--card-equal-height .card-footer {
  margin-top: auto;
}

.is-vcentered {
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
}
</style>