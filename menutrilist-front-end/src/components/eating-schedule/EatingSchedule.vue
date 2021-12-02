<template>
  <div class="q-pa-md q-mt-xl">
    <div class="row justify-center">
      <p class="col-7 text-bold text-h3 text-center q-mb-none">Wednesday</p>
      <p class="col-7 text-bold text-h5 text-center">24 November 2021</p>
    </div>
    <q-list bordered class="q-mt-xl q-mx-md" style="border-radius: 15px">
      <q-expansion-item
        expand-separator
        label="Breakfast"
        class="q-pa-sm text-bold text-h6"
      >
        <base-card>
          <q-card-section>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem, eius reprehenderit eos corrupti
            commodi magni quaerat ex numquam, dolorum officiis modi facere maiores architecto suscipit iste
            eveniet doloribus ullam aliquid.
          </q-card-section>
        </base-card>
      </q-expansion-item>

      <q-expansion-item
        expand-separator
        label="Lunch"
        class="q-pa-sm text-bold text-h6"
      >
        <base-card>
          <q-card-section>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem, eius reprehenderit eos corrupti
            commodi magni quaerat ex numquam, dolorum officiis modi facere maiores architecto suscipit iste
            eveniet doloribus ullam aliquid.
          </q-card-section>
        </base-card>
      </q-expansion-item>

      <q-expansion-item
        expand-separator
        label="Dinner"
        class="q-pa-sm text-bold text-h6"
      >
        <base-card>
          <q-card-section>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem, eius reprehenderit eos corrupti
            commodi magni quaerat ex numquam, dolorum officiis modi facere maiores architecto suscipit iste
            eveniet doloribus ullam aliquid.
          </q-card-section>
        </base-card>
      </q-expansion-item>
    </q-list>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BaseCard from 'src/components/ui/BaseCard.vue';
// import { EUserActions } from 'src/store/user/types/enumeration';


export default defineComponent({
  components: {
    BaseCard,
  },
});


</script>