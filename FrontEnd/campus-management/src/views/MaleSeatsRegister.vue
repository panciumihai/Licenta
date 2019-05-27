<template>
  <div class="adaugare-locuri">
    <v-container>
      <v-card>
        <v-card-title class="title">Alegeti caminele disponibile pentru BAIETI:</v-card-title>
        <v-card-text>
          <v-layout align-space-around justify-start column>
            <v-flex>
              <v-select
                v-model="hostelsStatus"
                :items="hostels"
                item-value="hostelId"
                item-text="name"
                attach
                chips
                label="Lista camine"
                multiple
                menu-props="offsetY"
                return-object
              ></v-select>
            </v-flex>
            <v-flex class="mt-3" v-show="hostelsStatus.length > 0">
              <div class="title">Introduceti numarul de locuri pentru caminele selectate:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(hostel) in hostelsStatus" :key="hostel.hostelId">
                  <v-text-field
                    class="mx-3"
                    :label="hostel.name"
                    v-model="hostel.totalMaleSeats"
                    mask="####"
                    @keyup="computeHostelSeats(hostel)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex md12 class="mt-3" v-show="hostelsStatus.length > 0">
              <div class="title">Ajustati procentul de locuri pentru rezerva:</div>
              <v-layout row align-center>
                <div class="title pb-4 mr-4 ml-2">0%</div>
                <v-slider
                  class="my-5"
                  v-model="slider"
                  thumb-label="always"
                  @change="sliderChanged"
                ></v-slider>
                <div class="title pb-4 mr-2 ml-4">100%</div>
              </v-layout>
            </v-flex>
            <v-flex v-show="hostelsStatus.length > 0">
              <div class="title mb-3 mt-2">Locurile pastrate pentru rezerva:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(hostel) in hostelsStatus" :key="hostel.hostelId">
                  <v-text-field
                    class="mx-3"
                    :label="hostel.name"
                    v-model="hostel.reservedMaleSeats"
                    mask="####"
                    v-on:keyup="reservedMaleSeatsChanged(hostel)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex v-show="hostelsStatus.length > 0">
              <div class="title mb-3 mt-2">Locurile finale disponbile:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(hostel) in hostelsStatus" :key="hostel.hostelId">
                  <v-text-field
                    class="mx-3"
                    :label="hostel.name"
                    v-model="hostel.maleSeats"
                    mask="####"
                    v-on:keyup="maleSeatsChanged(hostel)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex class="mt-3" v-show="hostelsStatus.length > 0">
              <v-layout justify-center>
                <v-btn dark color="blue" @click="addHostelsStatus">Salveaza locuri</v-btn>
              </v-layout>
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
      selectedHostels: [],
      hostelsStatus: [],
      slider: 10
    };
  },
  methods: {
    addHostelStatus() {
      this.hostelsStatus.forEach(hostel => {
        if ("totalMaleSeats" in hostel) {
          (hostel.totalMaleSeats = null),
            (hostel.maleSeats = null),
            (hostel.reservedMaleSeats = null);
        }
      });
    },

    computeHostelSeats(hostel) {
      hostel.maleSeats = Math.ceil(
        ((100 - this.slider) / 100) * hostel.totalMaleSeats
      );
      hostel.reservedMaleSeats = 0 + hostel.totalMaleSeats - hostel.maleSeats;
    },
    sliderChanged() {
      this.hostelsStatus.forEach(h => {
        if (h.totalMaleSeats != null) {
          h.maleSeats = Math.ceil(
            ((100 - this.slider) / 100) * h.totalMaleSeats
          );
          h.reservedMaleSeats = 0 + h.totalMaleSeats - h.maleSeats;
        }
      });
    },
    reservedMaleSeatsChanged(hostel) {
      if (Number(hostel.reservedMaleSeats) > Number(hostel.totalMaleSeats))
        hostel.reservedMaleSeats = hostel.totalMaleSeats;

      hostel.maleSeats = hostel.totalMaleSeats - hostel.reservedMaleSeats;
    },
    maleSeatsChanged(hostel) {
      if (Number(hostel.maleSeats) > Number(hostel.totalMaleSeats))
        hostel.maleSeats = hostel.totalMaleSeats;

      hostel.reservedMaleSeats = hostel.totalMaleSeats - hostel.maleSeats;
    },
    addHostelsStatus() {
      this.$store.dispatch("addOrUpdateHostelsStatus", this.hostelsStatus);
    }
  },
  computed: {
    hostels() {
      return this.$store.getters.hostels.map(function(h) {
        return {
          hostelId: h.id,
          name: h.name,
          totalMaleSeats: null,
          maleSeats: null,
          reservedMaleSeats: null
        };
      });
    }
  }
};
</script>

<style>
</style>
