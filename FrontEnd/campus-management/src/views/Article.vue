<template>
  <div class="article">
    <v-container>
      <v-card>
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
              <v-img :src="article.image" :lazy-src="article.image" aspect-ratio="2.75"></v-img>
            </v-flex>
            <v-flex xs12 md12 class="pb-3 px-3">
              <div class="title font-weight-regular" v-html="article.content"></div>
            </v-flex>
          </v-layout>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  data() {
    return {
      html_content:
        '<p>Apa curenta va fi oprita in intervalul orar care va<strong> fi cel care nu este prezen</strong>t aici si de aceia va ruga sa <span class="ql-size-large">fiti draguti si sa nu</span> va suparati ca altfel nu e bine multimi frumos si o zi frumoasa va odesc si asa mai departe pentru ca deja m-am lictisit si nu<span class="ql-size-large"> mai vreua sa mai s</span>criu altceva.</p><p><br></p><p><br></p><p><br></p><h1>multumesc inca odata si o zi cat mai placuta.</h1>'

      // news: {
      //   title: "Anunt important legat de cazarile de pe data de 22 septembrie",
      //   date: "22-02-2019",
      //   image: "http://campus.tuiasi.ro/wp-content/uploads/DSC_0173-min-1.jpg",
      //   author: "Popescu Marian",
      //   content:
      //     "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sed ex tempus, iaculis lacus eu, consectetur ligula. Proin pharetra, ante vitae congue placerat, ipsum lacus maximus tellus, pretium imperdiet tellus urna in arcu. Vivamus elementum fringilla ex at ultricies. Nam volutpat tortor eget nisl viverra venenatis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sed ex tempus, iaculis lacus eu, consectetur ligula. Proin pharetra, ante vitae congue placerat, ipsum lacus maximus tellus, pretium imperdiet tellus urna in arcu. Vivamus elementum fringilla ex at ultricies. Nam volutpat tortor eget nisl viverra venenatis."
      // }
    };
  },
  created() {
    this.$store.dispatch("getArticle", this.$route.params.id);
    console.log(this.$route.params.id);
  },
  computed: {
    article() {
      return this.$store.getters.article;
    }
  }
};
</script>

<style>
</style>
