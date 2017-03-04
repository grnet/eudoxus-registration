using System;
using System.Web.UI;
using Eudoxus.BusinessModel;


namespace Eudoxus.Portal.UserControls
{
    public partial class PublisherInput : UserControl
    {
        private Publisher _Publisher = null;

        private enPublisherType? _publisherType = null;
        public enPublisherType PublisherType
        {
            get
            {
                if (_publisherType.HasValue)
                    return _publisherType.Value;
                if (_Publisher != null)
                    return _Publisher.PublisherType;
                return enPublisherType.LegalPerson;
            }
            set
            {
                _publisherType = value;
            }
        }

        public bool IsForeignPublisher
        {
            get
            {
                bool? _isForeignPublisher = ViewState["__IsForeignPublisher"] as bool?;

                if (_isForeignPublisher.HasValue)
                    return _isForeignPublisher.Value;

                if (_Publisher != null)
                {
                    ViewState["__IsForeignPublisher"] = _Publisher.PublisherDetails.PrefectureID == HelpDeskConstants.FOREIGN_PREFECTURE;
                    return _Publisher.PublisherDetails.PrefectureID == HelpDeskConstants.FOREIGN_PREFECTURE;
                }

                return false;
            }
            set
            {
                ViewState["__IsForeignPublisher"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            switch (PublisherType)
            {
                case enPublisherType.LegalPerson:
                    if (IsForeignPublisher)
                    {
                        mv.SetActiveView(vForeignLegalPerson);
                    }
                    else
                    {
                        mv.SetActiveView(vLegalPerson);
                    }
                    break;
                case enPublisherType.SelfPublisher:
                    mv.SetActiveView(vSelfPublisher);
                    break;
                case enPublisherType.EbookPublisher:
                    mv.SetActiveView(vEbookPublisher);
                    break;
            }
            base.OnPreRender(e);
        }

        public void FillPublisher(Publisher publisher)
        {
            _Publisher = publisher;
            switch (PublisherType)
            {
                case enPublisherType.LegalPerson:
                    if (IsForeignPublisher)
                    {
                        flrInput.FillPublisher(publisher);
                    }
                    else
                    {
                        lrInput.FillPublisher(publisher);
                    }
                    break;
                case enPublisherType.SelfPublisher:
                    spInput.FillPublisher(publisher);
                    break;
                case enPublisherType.EbookPublisher:
                    epInput.FillPublisher(publisher);
                    break;
            }
        }

        public void SetPublisher(Publisher publisher)
        {
            _Publisher = publisher;
            switch (PublisherType)
            {
                case enPublisherType.LegalPerson:
                    if (IsForeignPublisher)
                    {
                        flrInput.SetPublisher(publisher);
                    }
                    else
                    {
                        lrInput.SetPublisher(publisher);
                    }
                    break;
                case enPublisherType.SelfPublisher:
                    spInput.SetPublisher(publisher);
                    break;
                case enPublisherType.EbookPublisher:
                    epInput.SetPublisher(publisher);
                    break;
            }
        }

        public int PublisherID
        {
            get
            {
                switch (PublisherType)
                {
                    case enPublisherType.LegalPerson:
                        if (IsForeignPublisher)
                        {
                            return flrInput.PublisherID;
                        }
                        else
                        {
                            return lrInput.PublisherID;
                        }
                    case enPublisherType.SelfPublisher:
                        return spInput.PublisherID;
                    case enPublisherType.EbookPublisher:
                        return epInput.PublisherID;
                }
                return 0;
            }
            set
            {
                switch (PublisherType)
                {
                    case enPublisherType.LegalPerson:
                        if (IsForeignPublisher)
                        {
                            flrInput.PublisherID = value;
                        }
                        else
                        {
                            lrInput.PublisherID = value;
                        }
                        break;
                    case enPublisherType.SelfPublisher:
                        spInput.PublisherID = value;
                        break;
                    case enPublisherType.EbookPublisher:
                        epInput.PublisherID = value;
                        break;
                }
            }
        }

        public bool ReadOnly
        {
            set
            {
                lrInput.ReadOnly = value;
                flrInput.ReadOnly = value;
                spInput.ReadOnly = value;
                epInput.ReadOnly = value;
            }
        }

        public string ValidationGroup
        {
            get { return lrInput.ValidationGroup; }
            set { lrInput.ValidationGroup = flrInput.ValidationGroup = spInput.ValidationGroup = epInput.ValidationGroup = value; }
        }

        public bool HelpDeskEditMode
        {
            set
            {
                lrInput.HelpDeskEditMode = value;
                flrInput.HelpDeskEditMode = value;
                spInput.HelpDeskEditMode = value;
                epInput.HelpDeskEditMode = value;
            }
        }

        public bool IsVerified { set { lrInput.IsVerified = flrInput.IsVerified = spInput.IsVerified = epInput.IsVerified = value; } }

        public bool IsVerifiedWithoutHasLogisticBooks { set { spInput.IsVerifiedWithoutHasLogisticBooks = value; } }
    }
}