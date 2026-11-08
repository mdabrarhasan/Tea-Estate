using TEALINK.Models.Generic;

namespace TEALINK.GARDEN.Interface.Mobile
{
    public interface ICommonRepo
    {
        public List<CommonModelResponseDetails> CommonDropDown(CommonModelRequest commonreq);
    }
}
