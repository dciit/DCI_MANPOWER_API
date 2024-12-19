using DCI_MANPOWER_API.Contexts;
using DCI_MANPOWER_API.Models;
using DCI_MANPOWER_API.Params;
using DCI_MANPOWER_API.Props;
using Microsoft.AspNetCore.Mvc;

namespace DCI_MANPOWER_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManpowerController : ControllerBase
    {
        private readonly DBSCM efSCM = new DBSCM();

        public ManpowerController(DBSCM efSCM)
        {
            this.efSCM = efSCM;
        }

        [HttpGet]
        [Route("/GetLayoutDetailByCode/{layoutCode}")]
        public IActionResult GetLayoutDetailByCode(string layoutCode)
        {
            MpckLayout mpckLayout = efSCM.MpckLayouts.FirstOrDefault(x => x.LayoutCode == layoutCode)!;
            return Ok(mpckLayout);
        }

        [HttpPost]
        [Route("/UpdateLayoutDetail")]
        public IActionResult UpdateLayoutDetial([FromBody] ParamUpdateLayoutDetail param)
        {
            string layoutCode = param.layoutCode;
            MpckLayout mpckLayout = efSCM.MpckLayouts.FirstOrDefault(x => x.LayoutCode == layoutCode)!;
            if (mpckLayout != null)
            {
                mpckLayout.LayoutName = param.layoutName;
                mpckLayout.Width = param.width;
                mpckLayout.Height = param.height;
                mpckLayout.LayoutStatus = param.layoutStatus;
                mpckLayout.BypassMq = param.bypassMQ;
                mpckLayout.BypassSa = param.bypassSA;
                mpckLayout.UpdateDate = DateTime.Now;
                mpckLayout.UpdateBy = param.updateBy;
                efSCM.MpckLayouts.Update(mpckLayout);
                int update = efSCM.SaveChanges();
                return Ok(new PropStatus()
                {
                    status = update > 0 ? true : false,
                    message = $"à¡Ô´¢éÍ¼Ô´¾ÅÒ´ÃĞËÇèÒ§ºÑ¹·Ö¡¢éÍÁÙÅ : {layoutCode}"
                });
            }
            else
            {
                return Ok(new PropStatus()
                {
                    status = false,
                    message = $"äÁè¾º¢éÍÁÙÅ¾×é¹·Õè : {layoutCode}"
                });
            }
        }

        [HttpPost]
        [Route("/AddPointMP")]
        public IActionResult AddPointMP([FromBody] ParamAddPointMP param)
        {
            Random rnd = new Random();
            string layoutCode = param.layoutCode;
            string objMasterID = param.objMasterID;
            string empcode = param.empcode;
            MpckObject newPoint = new MpckObject()
            {
                ObjCode = $"MP{DateTime.Now.ToString("yyyyMMdd")}{rnd.Next(0, 10000).ToString("D5")}",
                EmpCode = empcode,
                LayoutCode = layoutCode,
                ObjMasterId = objMasterID,
                ObjType = "MP",
                ObjTitle = param.objTitle,
                ObjSubtitle = param.objSubTitle,
                ObjPath = "",
                ObjX = 0,
                ObjY = 0,
                ObjStatus = "ACTIVE",
                ObjInsertDt = DateTime.Now,
                ObjWidth = 0,
                ObjHeight = 0,
                ObjBackgroundColor = "",
                ObjBorderColor = "",
                ObjBorderWidth = 1,
                ObjFontColor = "",
                ObjLastCheckDt = DateTime.Now,
                ObjPosition = "OP",
                ObjPicture = ""
            };
            efSCM.MpckObjects.Add(newPoint);
            int insert = efSCM.SaveChanges();
            return Ok(new PropStatus()
            {
                status = insert > 0 ? true : false,
                message = "",
            });
        }


    }
}