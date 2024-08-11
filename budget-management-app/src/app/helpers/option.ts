export class Option {
  name!: string;
  value!: string;

  constructor(init?: Partial<Option>) {
    Object.assign(this, init);
  }
}
