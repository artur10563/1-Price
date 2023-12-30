using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.ComplaintDTOs;
using OnePrice.UI.Models.PostDTOs;

namespace OnePrice.UI.Controllers
{
	public class ComplaintController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public ComplaintController(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> RenderComplaintModal()
		{
			var viewModel = new ComplaintAddViewModel
			{
				Complaint = new ComplaintAddDTO(),
				AvailableTypes = await _uow.ComplaintTypes.GetAll()
				.AsQueryable()
				.ProjectTo<ComplaintTypeDTO>(_mapper.ConfigurationProvider)
				.ToListAsync()
			};
			return PartialView("_ComplaintModalPartial", viewModel);
		}


		[HttpPost]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Create(ComplaintAddDTO newComplaint)
		{

			if (!ModelState.IsValid) return BadRequest("Complain info is not valid");

			var email = User?.FindFirst("email")?.Value;
			var user = await _uow.Users.GetByEmailAsync(email);
			if (user == null) return Unauthorized("Authorize to complain!");

			var complaint = _mapper.Map<Complaint>(newComplaint);
			var statusId = _uow.ComplaintStatuses.FirstOrDefault(s => s.Name == "Pending")?.Id;
			if (statusId == null) return NotFound("Try again later");

			complaint.AuthorId = user.Id;
			complaint.StatusId = (int)statusId;

			await _uow.Complaints.AddAsync(complaint);
			await _uow.SaveChangesAsync();

			return Ok("Complaint created successfully");
		}

	}
}

