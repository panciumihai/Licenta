<template>
  <v-container fill-height>
    <Loading v-if="!isLoaded"></Loading>
    <v-card v-else>
      <v-card-title class="mx-3 mt-3 mb-1 pb-1 display-2">{{ article.title }}</v-card-title>
      <v-card-text class="mt-1">
        <v-layout row wrap>
          <v-flex xs12 md12 class="pb-3 px-3">
            <v-layout align-start justify-start>
              <div class="subheading">{{article.authorFirstName + " " + article.authorLastName}}</div>
              <div class="subheading font-weight-thin ml-1">a postat pe {{article.postedDateTime}}</div>
            </v-layout>
          </v-flex>
          <v-flex xs12 md12 class="pb-3 px-3 mb-3">
            <v-img :src="article.image" aspect-ratio="2.75"></v-img>
          </v-flex>
          <v-flex xs12 md12 class="pb-3 px-3">
            <div class="title font-weight-regular" v-html="article.content"></div>
          </v-flex>
        </v-layout>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import Loading from "@/components/Loading";

export default {
  components: {
    Loading
  },
  mounted() {
    this.$store.dispatch("getArticle", this.$route.params.id);
    //console.log(this.$route.params.id);
  },
  computed: {
    isLoaded() {
      if (Object.keys(this.article).length > 0) return true;
      return false;
    },
    article() {
      return this.$store.getters.article;
    }
  }
};
</script>

<style>
.h2 {
  margin-bottom: 300px !important;
}
</style>
