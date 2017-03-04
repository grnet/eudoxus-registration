using System;
using System.Web.UI;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.UserControls
{
    public partial class DistributionPointInput : BaseEntityUserControl<DistributionPoint>
    {
        protected override void OnPreRender(EventArgs e)
        {
            switch (Entity.DistributionPointType)
            {
                case enDistributionPointType.Store:
                    mv.SetActiveView(vStore);
                    sdInput.Entity = Entity;
                    sdInput.Bind();
                    break;
                case enDistributionPointType.Institution:
                    mv.SetActiveView(vSecretary);
                    siInput.Entity = Entity;
                    sdInput.Bind();
                    break;
            }
            base.OnPreRender(e);
        }

        public override DistributionPoint Fill(DistributionPoint entity)
        {
            switch (entity.DistributionPointType)
            {
                case enDistributionPointType.Store:
                    sdInput.Fill(entity);
                    break;
                case enDistributionPointType.Institution:
                    siInput.Fill(entity);
                    break;
            }

            return entity;
        }

        public override void Bind()
        {
            switch (Entity.DistributionPointType)
            {
                case enDistributionPointType.Store:
                    sdInput.Entity = Entity;
                    sdInput.Bind();
                    break;
                case enDistributionPointType.Institution:
                    siInput.Entity = Entity;
                    siInput.Bind();
                    break;
            }
        }


        public bool ReadOnly
        {
            set
            {
                sdInput.ReadOnly = value;
                siInput.ReadOnly = value;
            }
        }

        public string ValidationGroup
        {
            get { return sdInput.ValidationGroup; }
            set { sdInput.ValidationGroup = siInput.ValidationGroup = value; }
        }
    }
}