<template>
  <div class="article_preview">
    <v-container>
      <v-card @click="goToDetails(article.id)">
        <v-layout row wrap>
          <v-flex xs12 md12 class="px-3 pt-3">
            <div class="title">{{ article.title }}</div>
          </v-flex>
          <v-flex xs12 md12 class="pb-3 px-3">
            <v-layout align-start justify-start>
              <div class="subheading">{{article.authorFirstName + " " + article.authorLastName}}</div>
              <div class="subheading font-weight-thin ml-1">a postat pe {{article.postedDateTime}}</div>
            </v-layout>
          </v-flex>
          <v-flex xs12 md12>
            <v-img :src="article.image" :lazy-src="article.image" aspect-ratio="2.75">
              <v-layout row fill-height wrap align-end>
                <v-flex xs12 sm12 md12 lg12>
                  <div class="transparent">
                    <div class="pa-3 body-2 white--text">
                      {{// eslint-disable-next-line
                      article.content.substring(0,200).replace(/<\/?[^>]+(>|$)/g, "")}}...
                      <div
                        class="blue--text text--lighten-4 body-2 d-inline-flex"
                      >Click pe postare pentru a vedea mai mult!</div>
                    </div>
                  </div>
                </v-flex>
              </v-layout>
            </v-img>
          </v-flex>
        </v-layout>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  props: {
    article: {}
  },
  data() {
    return {};
  },
  methods: {
    goToDetails(articleId) {
      this.$router.push({
        name: "article",
        params: { id: articleId }
      });
    }
  }
};
</script>

<style scoped>
.transparent {
  background-color: rgb(0, 0, 0, 0.65) !important;
  background: rgba(0, 0, 0, 0.65) !important;
}
</style>
