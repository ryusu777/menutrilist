<template>
    <q-page class="q-mx-lg">
        <base-card 
            class="row justify-center q-pa-md q-mt-xl"
            style="border-radius: 25px; background-color: #E8FCF8"
        >
            <div class="col-10 q-mt-lg" color="white">
                <base-select label="Serving" />
            </div>
            <div class="col-10">
                <div class="row justify-between q-ma-none">
                    <base-card-small class="q-pa-sm q-mt-lg col-5 text-center" style="border-radius: 25px">
                        <p class="q-mb-none">Calories</p>
                        <p class="q-mb-none text-caption">2gr</p>
                    </base-card-small>
                    <base-card-small class="q-pa-sm q-mt-lg col-6 text-center" style="border-radius: 25px">
                        <p class="q-mb-none">Protein</p>
                        <p class="q-mb-none text-caption">3mg</p>
                    </base-card-small>
                    <base-card-small class="q-pa-sm q-mt-lg col-6 text-center" style="border-radius: 25px">
                        <p class="q-mb-none">Carbohydrate</p>
                        <p class="q-mb-none text-caption">2gr</p>
                    </base-card-small>
                    <base-card-small class="q-pa-sm q-mt-lg col-5 text-center" style="border-radius: 25px">
                        <p class="q-mb-none">Protein</p>
                        <p class="q-mb-none">3mg</p>
                    </base-card-small>
                </div>
            </div>
            <div class="col-10 q-mt-lg">
                <div class="row justify-end">
                    <base-button label="Add" color="primary" />
                </div>
            </div>

        </base-card>
    </q-page>
</template>

<script>
import { defineComponent } from 'vue';
import BaseSelect from 'components/ui/BaseSelect.vue';
import BaseCard from 'src/components/ui/BaseCard.vue';
import BaseCardSmall from 'src/components/ui/BaseCardSmall.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';

export default defineComponent({
  components: {
    BaseSelect,
    BaseCardSmall,
    BaseCard,
    BaseButton,
    BaseSelect
  },
});
</script>