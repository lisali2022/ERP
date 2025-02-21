﻿using Volo.Abp.Application.Dtos;
using Yi.Framework.Bbs.Domain.Shared.Enums;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.Assignment;

public class AssignmentDefineGetListOutputDto : EntityDto<Guid>
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remarks { get; set; }

    /// <summary>
    /// 任务类型
    /// </summary>
    public AssignmentTypeEnum AssignmentType { get; set; }
    
    /// <summary>
    /// 任务需求类型
    /// </summary>
    public AssignmentRequirementTypeEnum AssignmentRequirementType{ get; set; }
    
    /// <summary>
    /// 总共步骤数
    /// </summary>
    public int TotalStepNumber { get; set; }

    /// <summary>
    /// 前置任务id
    /// </summary>
    public Guid? PreAssignmentId { get; set; }

    /// <summary>
    /// 任务奖励的钱钱数量
    /// </summary>
    public decimal RewardsMoneyNumber { get; set; }

    public long OrderNum { get; set; }
}