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
  methods: {
    compareByDate(a, b) {
      let comparison = 0;

      if (a.postedDateTime < b.postedDateTime) comparison = -1;
      else if (a.postedDateTime > b.postedDateTime) comparison = 1;

      return comparison;
    }
  },
  computed: {
    articles() {
      let articles = this.$store.getters.articles;
      return articles.sort(this.compareByDate);
    }
  },
  created() {
    this.$store.dispatch("getArticles");
  }
};
</script>
