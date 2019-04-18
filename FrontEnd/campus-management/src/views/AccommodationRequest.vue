<template>
  <div class="accommodation-request">
    <v-container>
      <v-card>
        <v-card-title class="display-1">{{ title }}</v-card-title>
        <v-card-text>
          <v-layout column wrap>
            <v-flex>
              <div class="title mt-3">Fiu / Fiica de cadru didactic?</div>
              <v-layout>
                <v-radio-group v-model="parinteProfesor" row>
                  <v-radio color="blue" label="Nu" value="0"></v-radio>
                  <v-radio color="blue" label="Da" value="1"></v-radio>
                </v-radio-group>
              </v-layout>
            </v-flex>
            <v-flex>
              <div class="title mt-3">Cazuri speciale? (Social / Medical / Orfan / Altele)</div>
              <v-layout>
                <v-checkbox color="blue" v-model="cazuriSpeciale" label="Social" value="Social"></v-checkbox>
                <v-checkbox color="blue" v-model="cazuriSpeciale" label="Medical" value="Medical"></v-checkbox>
                <v-checkbox color="blue" v-model="cazuriSpeciale" label="Orfan" value="Orfan"></v-checkbox>
                <v-checkbox color="blue" v-model="cazuriSpeciale" label="Altele" value="Altele"></v-checkbox>
              </v-layout>
            </v-flex>
            <v-flex>
              <div class="title mt-3">Bursier al statului roman?</div>
              <v-layout>
                <v-radio-group v-model="bursier" row>
                  <v-radio color="blue" label="Nu" value="0"></v-radio>
                  <v-radio color="blue" label="Da" value="1"></v-radio>
                </v-radio-group>
              </v-layout>
            </v-flex>
            <v-flex>
              <div class="title mt-3">Daca solicita cazare sau nu?</div>
              <v-layout>
                <v-radio-group v-model="cazare" row>
                  <v-radio color="blue" label="Nu" value="0"></v-radio>
                  <v-radio color="blue" label="Da" value="1"></v-radio>
                </v-radio-group>
              </v-layout>
            </v-flex>
            <v-flex>
              <div
                class="title mt-3"
              >Unde solicitati cazare? (Comisia va acorda cazare dupa ordinea optiunilor studentului)</div>
              <v-layout>
                <v-select
                  menu-props="offsetY"
                  v-model="preferinteAlese"
                  :items="listaPreferinteCamine"
                  item-value="nume"
                  item-text="nume"
                  attach
                  chips
                  label="Lista preferinte"
                  multiple
                ></v-select>
              </v-layout>
            </v-flex>
            <v-flex>
              <div class="title mt-3">Unde ai locuit in anul precedent?</div>
              <v-layout>
                <v-text-field label="Locuinta" v-model="locuintaPrecedenta"></v-text-field>
              </v-layout>
            </v-flex>
          </v-layout>
          <v-flex>
            <v-layout justify-center>
              <v-btn class="mt-3" dark color="blue">Trimite Cererea</v-btn>
            </v-layout>
          </v-flex>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  data() {
    return {
      title: "Formular pentru solicitare de cazare",
      parinteProfesor: "0",
      cazuriSpeciale: [],
      bursier: "0",
      cazare: "1",
      locuintaPrecedenta: "",
      preferinteAlese: [],
      listaPreferinteCamine: [
        { nume: "Akademos" },
        { nume: "Gaudeamus" },
        { nume: "C1" },
        { nume: "C2" },
        { nume: "C3" },
        { nume: "C4" },
        { nume: "C5" },
        { nume: "C6" },
        { nume: "C7" },
        { nume: "C8" },
        { nume: "C9" },
        { nume: "C10" },
        { nume: "C11" },
        { nume: "C12" }
      ]
    };
  }
};
</script>

<style>
</style>
