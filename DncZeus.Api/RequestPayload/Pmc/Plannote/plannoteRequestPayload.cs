/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Pmc.Plannote
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class PlannoteRequestPayload : RequestPayload
    {
        public int dutyStatus { get; set; }
        public int status { get; set; }
        public string department { get; set; }
        public string day { get; set; }
        public int diffItem { get; set; }
        public int focusItem { get; set; }
        public int detail { get; set; }
        

    }
}
