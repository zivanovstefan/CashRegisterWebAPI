using AutoMapper;
using AutoMapper.QueryableExtensions;
using CashRegister.Application.ErrorModels;
using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Common;
using CashRegister.Domain.Core.Bus;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Services
{
    public class BillService : IBillService
    {
        private readonly IMapper _mapper;
        private readonly IBillRepository _billRepository;
        private readonly IMediatorHandler _bus;
        public BillService(IMapper mapper, IBillRepository billRepository, IMediatorHandler bus)
        {
            _mapper = mapper;
            _billRepository = billRepository;
            _bus = bus;
        }
        public IEnumerable<BillVM> GetAllBills()
        {
            var bills = _billRepository.GetAllBills().ToList();
            var result = new List<BillVM>();
            foreach(var bill in bills)
            {
                result.Add(_mapper.Map<BillVM>(bill));
            }
            return result;
        }
        public ActionResult<bool> Create(BillVM billVM)
        {
            if (billVM == null)
            {
                return false;
            }
            var task = _bus.SendCommand(_mapper.Map<CreateBillCommand>(billVM));
            if (task == Task.FromResult(false))
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.BillAlreadyExist,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                };
                return new BadRequestObjectResult(errorResponse);
            }
            return true;
        }
        public ActionResult<bool> Update(BillVM billVM)
        {
            if (billVM == null)
            {
                return false;
            }
            var task = _bus.SendCommand(_mapper.Map<UpdateBillCommand>(billVM));
            if (task == Task.FromResult(false))
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.BillAlreadyExist,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                };
                return new BadRequestObjectResult(errorResponse);
            }
            return true;
        }
        public ActionResult<bool> Delete(string billNumber)
        {
            if (billNumber == "")
            {
                return false;
            }
            var bill = _billRepository.GetAllBills().FirstOrDefault(x => x.BillNumber == billNumber);
            _billRepository.Delete(bill);
            return true;
        }
        public ActionResult<BillVM> GetBillByID(string billNumber)
        {
            if (billNumber == "")
            {
                return null;
            }
            var bill = _billRepository.GetAllBills().FirstOrDefault(x => x.BillNumber == billNumber);
            if(bill == null)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.Bill_Does_Not_Exist,
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return new BadRequestObjectResult(errorResponse);
            }
            var result = _mapper.Map<BillVM>(bill);
            return result;
        }
    }
}
