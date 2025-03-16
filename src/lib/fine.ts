import { FineClient } from "@fine-dev/fine-js";

export const fine = new FineClient("https://baas-cn-maroon-array-senior-stone-entropy.on.fine.dev", {
  autoRefreshToken: true,
  persistSession: true,
});