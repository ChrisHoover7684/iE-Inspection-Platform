export const EXTERNAL_REPORT_CHECKLIST_FIELDS = [
  'Create Finding',
  'Recommendation Required',
  'Repair Required',
  'Photo Required',
  'NDE Required',
  'Add to Summary'
] as const;

export const EXTERNAL_REPORT_SUMMARY_CHECKLIST_FIELDS = EXTERNAL_REPORT_CHECKLIST_FIELDS.filter((field) => field !== 'Add to Summary');

export const buildChecklistCounts = <TField extends string>(
  fields: readonly TField[],
  items: readonly string[],
  predicate: (item: string, field: TField) => boolean
): Record<TField, number> => Object.fromEntries(
  fields.map((field) => [field, items.filter((item) => predicate(item, field)).length])
) as Record<TField, number>;
