# configuration

**DAYBREAK** implements its own configuration API inspired by systems much more capable than `ModConfig`.

It is designed with more esoteric and flexible scenarios in mind:

- provides support for syncing config entries across client-server boundaries when the mod is NoSync (assuming DAYBREAK is present on both sides),
- promotes defining config entries next to implementing code instead of in an unrelated code file or class definition,
- all behavior is configurable through function pointers,
  - this includes convenient support for arbitrary file and network serialization,
- non-global "config repositories" for when you need specially-scoped configs (such as configs dynamically generated at runtime and not managed by the mod loader),
- decoupling of UI and config entries, allowing UI to be a *consumer* of config data and metadata rather than an owner or provider,
- better cross-mod support through *handles*, which allow you to reference a config entry without a direct reference to an assembly.

## features

TODO

Talk about "value providers":

- validation hooks,
- clamping,
- lazy transformation,
- side-aware logic.

Talk about syncing:

- NoSync mods can properly communicate.

Talk about serialization:

- unified through Newtonsoft.JSON LINQ API but transformed to TOML and NBT for efficient human-readable representations and network transfer respectively.
