export const buildChecklistCounts = <TField extends string>(
  fields: readonly TField[],
  items: readonly string[],
  predicate: (item: string, field: TField) => boolean
): Record<TField, number> => Object.fromEntries(
  fields.map((field) => [field, items.filter((item) => predicate(item, field)).length])
) as Record<TField, number>;
