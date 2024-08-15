/// Maded by Gorox for Enterprise. See CLA
using System.Linq;
using Content.Shared.Fishing.Components;
using Content.Shared.DoAfter;
using Content.Shared.Interaction;
using Content.Shared.Interaction.Events;
using Robust.Server.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Audio.Systems;

namespace Content.Server.Fishing;

public sealed class FishingSystem : EntitySystem
{
    [Dependency] private readonly MetaDataSystem _metaData = default!;
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfterSystem = default!;
    [Dependency] private readonly TransformSystem _transformSystem = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<FishingRodComponent, AfterInteractEvent>(OnAfterInteract);
        SubscribeLocalEvent<FishingRodComponent, FishingDoAfterEvent>(OnDoAfter);
    }

    private void OnAfterInteract(Entity<FishingRodComponent> uid, ref AfterInteractEvent args)
    {
      if (args.Target != null && EntityManager.HasComponent<PondComponent>(args.Target))
      {

        var target = args.Target.Value;

        _audio.PlayPvs("/Audio/_ENT/Items/Fishing/castfast.ogg", uid);

        _doAfterSystem.TryStartDoAfter(new DoAfterArgs(EntityManager, args.User, 60, new FishingDoAfterEvent(), uid, target: args.Target, used: uid)
        {
            NeedHand = true,
            BreakOnMove = true
        });
      }
    }

    private void OnDoAfter(EntityUid uid, FishingRodComponent component, ref FishingDoAfterEvent args)
    {
       if (args.Handled || args.Args.Target == null || args.Cancelled)
           return;

       var target = args.Args.Target.Value;

       if (args.User != null && TryComp<PondComponent>(target, out var pondComp))
       {
           Spawn(pondComp.FishPrototype, Transform(args.User).Coordinates);
           _audio.PlayPvs("/Audio/_ENT/Items/Fishing/retrieve.ogg", uid);
       }
    }
}
