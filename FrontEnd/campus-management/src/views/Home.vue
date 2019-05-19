<template>
  <div class="home">
    <ArticlePreview v-for="article in articles" :key="article.title" :article="article"></ArticlePreview>
  </div>
</template>

<script>
import ArticlePreview from "@/components/ArticlePreview";

export default {
  components: {
    ArticlePreview
  },
  data() {
    return {};
  },
  computed: {
    articles() {
      return this.$store.getters.articles;
    }
  },
  created() {
    this.$store.dispatch("getArticles");
  }
};
</script>
